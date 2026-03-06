import { NavButton } from '../components/NavButton';

// Tela inicial da aplicação, basicamente um hub com botões de navegação
export function ControlSystemHub() {
    return (
        <div className="container">
            <div className='title'>
                <h1>Sistema de controle financeiro</h1>
                <h3>Escolha o que deseja fazer:</h3>
            </div>
            <nav className='center-form' style={{ padding: "20px" }}>
                {/* navega para a tela de listagem de usuários */}
                <NavButton className='navButton' to="/users" label="Usuários" />

                {/* navega para a tela de listagem de transações */}
                <NavButton className='navButton' to="/transactions" label="Transações" />

                {/* navega para a tela de listagem de categorias */}
                <NavButton className='navButton' to="categories" label="Categorias" />
                
                {/* navega para a tela do sumário de transações */}
                <NavButton className='navButton' to="/financialsummary" label="Sumário de transações" />
            </nav>
        </div>
    );
}