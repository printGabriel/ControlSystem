
import { useEffect, useRef, useState } from 'react';
import { api } from '../services/api'
import { useNavigate } from 'react-router-dom';
import '../App.css'

interface Transaction {
    id: number;
    description: string;
    value: number;
    transactionType: number;
    categoryId: number;
    userId: number;
}

export function Transactions() {
    const [transactions, setTransactions] = useState<Transaction[]>([])

    async function getTransactions() {
        const transactionsApi = await api.get('/transactions/get-all-transactions')

        setTransactions(transactionsApi.data);
    }

    async function deleteTransaction(id: number) {
        await api.delete(`/transactions/${id}`);
        setTransactions(transactions.filter(transaction => transaction.id !== id));
    }

    const navigate = useNavigate();

    function editTransaction(id: number) {
        navigate(`/transactionsForm/${id}`);
    }

    function navi(type: number) {

        switch (type) {
            case 1: navigate(`/`); break;
            case 2: navigate(`/transactionsForm/`); break;
        }
    }

    useEffect(() => {
        getTransactions()
    }, [])

    return ( 
        <div className="container-register">
            <div>
                <button style={{ marginRight: "20px", marginBottom: "10px" }} type="button" onClick={() => navi(2)}>Adicionar Transação</button>
                <button style={{ marginRight: "538px", marginBottom: "10px", width: "200px" }} type="button" onClick={() => navi(1)}>Início</button>
            </div>
            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Id:</th>
                            <th>Descrição:</th>
                            <th>Valor:</th>
                            <th>Tipo de transação</th>
                            <th>Id da categoria</th>
                            <th>Id do usuário</th>
                            <th>Editar</th>
                            <th>Excluir</th>
                        </tr>
                    </thead>
                    <tbody>
                        {transactions.map((transaction) => (
                            <tr key={transaction.id}>
                                <td>{transaction.id}</td>
                                <td>{transaction.description}</td>
                                <td>{transaction.value}</td>
                                <td>{transaction.transactionType}</td>
                                <td>{transaction.categoryId}</td>
                                <td>{transaction.userId}</td>
                                <td>
                                    <button onClick={() => editTransaction(transaction.id)}>
                                        Editar
                                    </button>
                                </td>
                                <td>
                                    <button className="deleteButton" onClick={() => deleteTransaction(transaction.id)}>
                                        Deletar
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}