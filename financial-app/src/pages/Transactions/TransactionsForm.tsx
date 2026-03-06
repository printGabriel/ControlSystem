import { useRef, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../../components/NavButton';
import { api } from '../../services/api'
import '../../App.css'

//interfaces para informar a tipagem das propriedades
interface User {
    id: number;
    name: string;
}

interface Category {
    id: number;
    description: string;
}

// formulário utilizado para adicionar uma nova transação
export function TransactionsForm() {
    // const para receber o valor de um id caso a tela seja acessada para edição
    const { id } = useParams();
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputValue = useRef<HTMLInputElement>(null);
    const inputTransactionType = useRef<HTMLSelectElement>(null);
    const inputCategoryId = useRef<HTMLSelectElement>(null);
    const inputUserId = useRef<HTMLSelectElement>(null);
    const [users, setUsers] = useState<User[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    //chamada com loadData para mapear erros e evitar que outros gets sejam chamados
    useEffect(() => {
        async function loadData() {
            try {
                await getUsers();
            } catch (error) {
                console.error("Erro ao buscar usuários", error);
            }

            try {
                await getCategories();
            } catch (error) {
                console.error("Erro ao buscar categorias", error);
            }

            //caso a tela seja aberta com um id, já busca essa transação
            if (id) {
                await getTransactionById();
            }
        }

        loadData();
    }, [id]);

    //return principal da função, formulário de registro de transação
    return (
        <div className="container-register">
            <form className="center-form">
                <h1>Cadastro de Transações</h1>
                <input name="Description" type="text" placeholder="Descrição" ref={inputDescription} />
                <input name="Value" type="number" placeholder="Valor" ref={inputValue} />

                {/* dropdown para registra o tipo de transação */}
                <select className='dropdown' defaultValue={""} ref={inputTransactionType}>
                    <option value="">Escolha o tipo de transação:</option>
                    <option value="1">1 - Despesa</option>
                    <option value="2">2 - Receita</option>
                </select>

                {/* Dropdown populado dinamicamente com categorias vindas do banco */}
                <select ref={inputCategoryId} defaultValue="">
                    <option value="">Selecione uma categoria</option>
                    {categories.map(category => (
                        <option key={category.id} value={category.id}>
                            {category.id} - {category.description}
                        </option>
                    ))}
                </select>

                {/* Dropdown populado dinamicamente com usuários vindos do banco */}
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

                {/* botões de navegção para a voltar a tela anterior e voltar para o início */}
                <NavButton className="navButtons" to="/transactions" label="Voltar" />
                <NavButton className="navButtons" to="/" label="Início" />
            </form>
        </div>
    );

    // função utilizada para buscar pela transação caso a tela tenha sido chamado com um id
    async function getTransactionById() {
        const response = await api.get(`/transactions/${id}`);

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

    //função para buscar usuários para o dropdown de usuários
    async function getUsers() {
        const response = await api.get('/users/');
        setUsers(response.data);
    }


    //função para buscar categorias para o dropdown de categorias
    async function getCategories() {
        const response = await api.get('/categories/');
        setCategories(response.data);
    }

    //função para salvar as ações na tela
    async function saveTransaction() {
        let transactionData = {};
        var create = null;

        //caso o usuário não tenha preenchido um campo, um alerta é disparado
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

        //condição abaixo segue a mesma ideia da tela de usuários para registrar ou editar uma transação
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

        //caso a tela tenha sido chamada com um id, atualiza o registro dessa transação.
        //aqui também retorna erro caso o usuário seja menor de 18 anos e tente registrar uma receita,
        //ou caso a transação não esteja de acordo com a categoria marcada.
        if (id) {
            try {
                await api.put(`/transactions/${id}`, transactionData);
            } catch (error: any) {
                alert(error.response.data)
            }

            //caso a tela seja aberta sem id, a ação é de registrar, então registra uma nova transação
        } else {
            try {
                create = await api.post('/transactions/', transactionData);
            } catch (error: any) {
                alert(error.response.data)
            }
        }

        //caso seja criação, limpa os campos por meio da função abaixo.
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