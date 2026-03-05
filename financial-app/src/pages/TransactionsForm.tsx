import { useRef, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../components/NavButton';
import { api } from '../services/api'
import '../App.css'

export function TransactionsForm() {
    const { id } = useParams();
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputValue = useRef<HTMLInputElement>(null);
    const inputTransactionType = useRef<HTMLInputElement>(null);
    const inputCategoryId = useRef<HTMLInputElement>(null);
    const inputUserId = useRef<HTMLInputElement>(null);

    useEffect(() => {
        if (id) {
            getTransactionById();
        }
    }, [id]);

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

    async function saveTransaction() {
        let transactionData = {};

        if (id) {
            transactionData = {
                Id: Number(id),
                Description: inputDescription.current?.value,
                Value: inputValue.current?.value,
                TransactionType: inputTransactionType.current?.value,
                CategoryId: inputCategoryId.current?.value,
                UserId: inputUserId.current?.value

            };
        } else {
            transactionData = {
                Description: inputDescription.current?.value,
                Value: inputValue.current?.value,
                TransactionType: inputTransactionType.current?.value,
                CategoryId: inputCategoryId.current?.value,
                UserId: inputUserId.current?.value
            }
        }

        if (id) {
            await api.put(`/transactions/update-transaction-by-id/${id}`, transactionData);
        } else {
            await api.post('/transactions/create-user', transactionData);
        }

        clearFields();
    }

    function clearFields() {
        if (inputDescription.current) inputDescription.current.value = "";
        if (inputValue.current) inputValue.current.value = "";
        if (inputTransactionType.current) inputTransactionType.current.value = "";
        if (inputCategoryId.current) inputCategoryId.current.value = "";
        if (inputUserId.current) inputUserId.current.value = "";
    }

    return (

        <div className="container-register">
            <form className="center-form">
                <h1>Cadastro de Transações</h1>
                <input name="Description" type="text" placeholder="Descrição" ref={inputDescription} />
                <input name="Value" type="number" placeholder="Valor" ref={inputValue} />
                <input name="TransactionType" type="number" placeholder="Tipo de transação" ref={inputTransactionType} />
                <input name="CategoryId" type="number" placeholder="Id da categoria" ref={inputCategoryId} />
                <input name="UserId" type="number" placeholder="Id do usuário" ref={inputUserId} />
     
                <button type="button" onClick={saveTransaction}>
                    {id ? "Atualizar" : "Registrar"}
                </button>

                <NavButton className="navButtons" to="/transactions" label="Voltar" />
                <NavButton className="navButtons" to="/" label="Início" />
            </form>
        </div>
    );
}