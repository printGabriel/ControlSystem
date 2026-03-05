import { useEffect, useState } from "react";
import { api } from "../services/api";
import '../App.css'

interface SummaryDto {
  userId: number;
  userName: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

interface FinancialSummaryResponse {
  users: SummaryDto[];
  totalIncome: number;
  totalExpense: number;
  totalBalance: number;
}

export function FinancialSummary() {
  const [data, setData] = useState<FinancialSummaryResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api.get("/users/financial-summary")
      .then(response => setData(response.data))
      .catch(() => setError("Error loading data"));
  }, []);

  if (error) return <p>{error}</p>;
  if (!data) return <p>Loading...</p>;

  return (
    <div style={{ padding: "20px" }}>
      <h2>Financial Summary</h2>

      {data.users.map(user => (
        <div key={user.userId} style={{ marginBottom: "15px" }}>
          <strong>{user.userName}</strong>
          <p>Income: {user.totalIncome}</p>
          <p>Expense: {user.totalExpense}</p>
          <p>Balance: {user.balance}</p>
          <hr />
        </div>
      ))}

      <h3>Grand Total</h3>
      <p>Total Income: {data.totalIncome}</p>
      <p>Total Expense: {data.totalExpense}</p>
      <p>Total Balance: {data.totalBalance}</p>
    </div>
  );
}