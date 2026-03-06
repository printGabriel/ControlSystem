import { useEffect, useState } from "react";
import { api } from "../../services/api";
import { useNavigate } from 'react-router-dom';
import '../../App.css'

//interface para tipar as propriedades dos objetos vindos do banco.
interface SummaryDto {
  userId: number,
  userName: string,
  totalIncome: number,
  totalExpense: number,
  balance: number
}

interface Summary {
  users: SummaryDto[],
  totalIncome: number,
  totalExpense: number,
  totalBalance: number
}

// tabela de apresentação do sumário financeiro
export function FinancialSummary() {
  const [summaries, setSummaries] = useState<Summary | null>(null)
  const navigate = useNavigate();

  useEffect(() => {
    getSummaries();
  }, []);

  //returno da função principal com tabela de informações de usuários e informações de valores gerais
  return (
    <div>
      <div className="summary-all">
        <h2>Receita Geral: {summaries?.totalIncome.toLocaleString('pt-BR', {
          style: 'currency',
          currency: 'BRL'
        })}</h2>
        <h2>Despesa Geral: {summaries?.totalExpense.toLocaleString('pt-BR', {
          style: 'currency',
          currency: 'BRL'
        })}</h2>
        <h2>Saldo Geral: {summaries?.totalBalance.toLocaleString('pt-BR', {
          style: 'currency',
          currency: 'BRL'
        })}</h2>
      </div>
      <div className="container-register">
        <table>
          <thead>
            <tr>
              <th>Id do usuário</th>
              <th>Nome</th>
              <th>Receita total</th>
              <th>Despesa total</th>
              <th>Saldo</th>
            </tr>
          </thead>
          <tbody>
            {summaries?.users.map((summary) => (
              <tr key={summary.userId}>
                <td>{summary.userId}</td>
                <td>{summary.userName}</td>
                {/* formatação para $real */}
                <td>{summary.totalIncome.toLocaleString('pt-BR', {
                  style: 'currency',
                  currency: 'BRL'
                })}</td>
                <td>{summary.totalExpense.toLocaleString('pt-BR', {
                  style: 'currency',
                  currency: 'BRL'
                })}</td>
                <td>{summary.balance.toLocaleString('pt-BR', {
                  style: 'currency',
                  currency: 'BRL'
                })}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div>
        <button className="buttonReturn" type="button" onClick={returnHome}>Início</button>
      </div>
    </div>
  );

    // função para voltar ao início
  function returnHome() {
    navigate("/");
  }

  //função utilizada para buscar pelas informações de sumário
  async function getSummaries() {
    const summaries = await api.get("/users/financial-summary");

    setSummaries(summaries.data);
  }
}