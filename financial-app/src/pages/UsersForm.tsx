
import { useRef } from 'react';
import { NavButton } from '../components/NavButton';
import { api } from '../services/api'
import '../App.css'

export function UsersForm() {
    const inputName = useRef<HTMLInputElement>(null);
    const inputEmail = useRef<HTMLInputElement>(null);
    const inputBirthDate = useRef<HTMLInputElement>(null);

    async function createUser() {
        await api.post('/users',{
            Name: inputName.current?.value,
            Email: inputEmail.current?.value,
            BirthDate: inputBirthDate.current?.value
        });
    }

    return (

        <div className="container-register">

            <form className="center-form">
                <h1>Cadastro de usuários</h1>
                <input name="name" type="text" placeholder="Nome" ref={inputName} />
                <input name="email" type="email" placeholder="Email" ref={inputEmail} />
                <input name="birthdate" type="date" ref={inputBirthDate} />
                <button type="button" onClick={createUser}>Registrar</button>
                <NavButton className="navButtons" to="/" label="Home" />
            </form>
        </div>
    );
}