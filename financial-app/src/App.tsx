import { ControlSystemHub } from "./pages/ControlSystemHub";
import { Routes, Route } from "react-router-dom";
import { UsersForm } from "./pages/UsersForm";
//import { FinancialSummary } from "./pages/FinancialSummary";
import { TransactionsForm } from "./pages/TransactionsForm";

function App() {
  return (
    <Routes>
      <Route path="/" element={<ControlSystemHub />} />
      <Route path="/usersform" element={<UsersForm />} />
      {/* <Route path="/financialsummary" element={<FinancialSummary />} /> */}
      <Route path="/transactionsform" element={<TransactionsForm />} />
    </Routes>
  );
}


export default App;