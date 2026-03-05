
import { useEffect, useRef, useState } from 'react';
import { api } from '../../services/api'
import { useNavigate } from 'react-router-dom';
import '../../App.css'

interface Transaction {
    id: number;
    description: string;
    value: number;
    transactionTypeName: string;
    categoryName: string;
    userName: string;
}

export function Transactions() {
    const [transactions, setTransactions] = useState<Transaction[]>([])
    const navigate = useNavigate();

    useEffect(() => {
        getTransactions()
    }, [])

    return (
        <div>
            <div className="container-register">
                <table>
                    <thead>
                        <tr>
                            <th>Id:</th>
                            <th>Descrição:</th>
                            <th>Valor:</th>
                            <th>Tipo de transação</th>
                            <th>Categoria</th>
                            <th>Usuário</th>
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
                                <td>{transaction.transactionTypeName == "Income" ? "Receita" : "Despesa"}</td>
                                <td>{transaction.categoryName}</td>
                                <td>{transaction.userName}</td>
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
            <div>
                <button className="buttonAdd" type="button" onClick={() => navi(2)}>Adicionar Transação</button>
                <button className="buttonReturn" type="button" onClick={() => navi(1)}>Início</button>
            </div>
        </div>
    );
    
    async function getTransactions() {
        const transactionsApi = await api.get('/transactions/get-all-transactions')

        setTransactions(transactionsApi.data);
    }

    async function deleteTransaction(id: number) {
        await api.delete(`/transactions/${id}`);
        setTransactions(transactions.filter(transaction => transaction.id !== id));
    }

    function editTransaction(id: number) {
        navigate(`/transactionsForm/${id}`);
    }

    function navi(type: number) {

        switch (type) {
            case 1: navigate(`/`); break;
            case 2: navigate(`/transactionsForm/`); break;
        }
    }

}