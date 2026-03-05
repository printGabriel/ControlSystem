import { ControlSystemHub } from "./pages/ControlSystemHub";
import { Routes, Route } from "react-router-dom";
import { UsersForm } from "./pages/Users/UsersForm";
import { FinancialSummary } from "./pages/Summaries/FinancialSummary";
import { TransactionsForm } from "./pages/Transactions/TransactionsForm";
import { CategoryForm } from "./pages/Categories/CategoryForm";
import { Users } from "./pages/Users/Users";
import { Categories } from "./pages/Categories/Categories";
import { Transactions } from "./pages/Transactions/Transactions";

function App() {
  return (
    <Routes>
      {/* Início */}
      <Route path="/" element={<ControlSystemHub />} />

       {/* Rotas usuários */}
      <Route path="/usersform" element={<UsersForm />} />
      <Route path="/users" element={<Users />} />
      <Route path="/users/:id" element={<UsersForm />} />

       {/* Rota sumários */}
      <Route path="/financialsummary" element={<FinancialSummary />} />

       {/* Rotas transações */}
      <Route path="/transactionsform" element={<TransactionsForm />} />
      <Route path="/transactions" element={<Transactions />} />
      <Route path="/transactionsform/:id" element={<TransactionsForm />} />

       {/* Rotas categorias */}
      <Route path="/categoriesform" element={<CategoryForm />} />
      <Route path="/categories" element={<Categories />} />
      <Route path="/categoriesform/:id" element={<CategoryForm />} />
    </Routes>
  );
}


export default App;