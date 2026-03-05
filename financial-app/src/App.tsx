import { ControlSystemHub } from "./pages/ControlSystemHub";
import { Routes, Route } from "react-router-dom";
import { UsersForm } from "./pages/UsersForm";
//import { FinancialSummary } from "./pages/FinancialSummary";
import { TransactionsForm } from "./pages/TransactionsForm";
import { CategoryForm } from "./pages/CategoryForm";
import { Users } from "./pages/Users";
import { Categories } from "./pages/Categories";
import { Transactions } from "./pages/Transactions";

function App() {
  return (
    <Routes>
      <Route path="/" element={<ControlSystemHub />} />
      <Route path="/usersform" element={<UsersForm />} />
      {/* <Route path="/financialsummary" element={<FinancialSummary />} /> */}
      <Route path="/transactionsform" element={<TransactionsForm />} />
      <Route path="/categoryform" element={<CategoryForm />} />
      <Route path="/users" element={<Users />} />
      <Route path="/categories" element={<Categories />} />
      <Route path="/transactions" element={<Transactions />} />
      <Route path="/users/:id" element={<UsersForm />} />
    </Routes>
  );
}


export default App;