import { useRef, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../../components/NavButton';
import { api } from '../../services/api'
import '../../App.css'
interface User {
    id: number;
    name: string;
}

interface Category {
    id: number;
    description: string;
}

export function TransactionsForm() {
    const { id } = useParams();
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputValue = useRef<HTMLInputElement>(null);
    const inputTransactionType = useRef<HTMLSelectElement>(null);
    const inputCategoryId = useRef<HTMLSelectElement>(null);
    const inputUserId = useRef<HTMLSelectElement>(null);
    const [users, setUsers] = useState<User[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    useEffect(() => {
        async function loadData() {
            await getUsers();
            await getCategories();

            if (id) {
                await getTransactionById();
            }
        }

        loadData();
    }, [id]);

    return (
        <div className="container-register">
            <form className="center-form">
                <h1>Cadastro de Transações</h1>
                <input name="Description" type="text" placeholder="Descrição" ref={inputDescription} />
                <input name="Value" type="number" placeholder="Valor" ref={inputValue} />
                <select className='dropdown' defaultValue={""} ref={inputTransactionType}>
                    <option value="">Escolha o tipo de transação:</option>
                    <option value="1">1 - Despesa</option>
                    <option value="2">2 - Receita</option>
                </select>
                <select ref={inputCategoryId} defaultValue="">
                    <option value="">Selecione uma categoria</option>
                    {categories.map(category => (
                        <option key={category.id} value={category.id}>
                            {category.id} - {category.description}
                        </option>
                    ))}
                </select>
                <select ref={inputUserId} defaultValue="">
                    <option value="">Selecione um usuário</option>
                    {users.map(user => (
                        <option key={user.id} value={user.id}>
                            {user.id} - {user.name}
                        </option>
                    ))}
                </select>

                <button type="button" onClick={saveTransaction}>
                    {id ? "Atualizar" : "Registrar"}
                </button>

                <NavButton className="navButtons" to="/transactions" label="Voltar" />
                <NavButton className="navButtons" to="/" label="Início" />
            </form>
        </div>
    );

    async function getTransactionById() {
        const response = await api.get(`/transactions/get-transaction-by-id/${id}`);

        if (inputDescription.current)
            inputDescription.current.value = response.data.description;

        if (inputValue.current)
            inputValue.current.value = response.data.value;

        if (inputTransactionType.current)
            inputTransactionType.current.value = response.data.transactionType;

        if (inputCategoryId.current)
            inputCategoryId.current.value = response.data.categoryId;

        if (inputUserId.current)
            inputUserId.current.value = response.data.userId;
    }

    async function getUsers() {
        const response = await api.get('/users/get-all-users');
        setUsers(response.data);
    }

    async function getCategories() {
        const response = await api.get('/categories/get-all-categories');
        setCategories(response.data);
    }

    async function saveTransaction() {
        let transactionData = {};
        var create = null;

        if (
            inputDescription.current?.value == ""
            || inputValue.current?.value == ""
            || inputTransactionType.current?.value == ""
            || inputCategoryId.current?.value == ""
            || inputUserId.current?.value == ""
        ) {
            alert("Preencha todos os campos!")
            return;
        }

        if (id) {
            transactionData = {
                Id: Number(id),
                Description: inputDescription.current?.value,
                Value: Number(inputValue.current?.value),
                TransactionType: Number(inputTransactionType.current?.value),
                TransactionTypeName: "",
                CategoryId: Number(inputCategoryId.current?.value),
                UserId: Number(inputUserId.current?.value),
                CategoryName: "",
                UserName: ""
            };
        } else {
            transactionData = {
                Description: inputDescription.current?.value,
                Value: Number(inputValue.current?.value),
                TransactionType: Number(inputTransactionType.current?.value),
                TransactionTypeName: "",
                CategoryId: inputCategoryId.current?.value,
                CategoryName: "",
                UserId: inputUserId.current?.value,
                UserName: ""
            }
        }

        if (id) {
            try {
                await api.put(`/transactions/update-transaction-by-id/${id}`, transactionData);
            } catch (error: any) {
                alert(error.response.data)
            }


        } else {
            try {
                create = await api.post('/transactions/create-transaction', transactionData);
            } catch (error: any) {
                alert(error.response.data)
            }
        }

        if (create != null)
            clearFields();
    }

    function clearFields() {
        if (inputDescription.current) inputDescription.current.value = "";
        if (inputValue.current) inputValue.current.value = "";
        if (inputTransactionType.current) inputTransactionType.current.value = "";
        if (inputCategoryId.current) inputCategoryId.current.value = "";
        if (inputUserId.current) inputUserId.current.value = "";
    }
}