
import { useRef } from 'react';
import { NavButton } from '../components/NavButton';
import { api } from '../services/api'
import '../App.css'

export function TransactionsForm() {
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputValue = useRef<HTMLInputElement>(null);
    const inputTransactionType = useRef<HTMLInputElement>(null);
    const inputCategoryId = useRef<HTMLInputElement>(null);
    const inputUserId = useRef<HTMLInputElement>(null);

    async function createTransaction() {
        await api.post('/transactions/create-transaction', {
            Description: inputDescription.current?.value,
            Value: inputValue.current?.value,
            TransactionType: inputTransactionType.current?.value,
            CategoryId: inputCategoryId.current?.value,
            UserId: inputUserId.current?.value,
        });
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
                <button type="button" onClick={createTransaction}>Registrar</button>
                <NavButton className="navButtons" to="/" label="Home" />
            </form>
        </div>
    );
}