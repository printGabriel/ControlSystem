import { NavButton } from '../components/NavButton';

export function ControlSystemHub() {
    return (
        <div className="container">
            <div className='title'>
                <h1>Sistema de controle financeiro</h1>
                <h3>Escolha o que deseja fazer:</h3>
            </div>
            <nav className='center-form' style={{ padding: "20px" }}>
                <NavButton className='navButton' to="/users" label="Usuários" />
                <NavButton className='navButton' to="/transactions" label="Transações" />
                <NavButton className='navButton' to="categories" label="Categorias" />
                <NavButton className='navButton' to="/financialsummary" label="Sumário de transações" />
            </nav>
        </div>
    );
}