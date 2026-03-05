import { useRef, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../components/NavButton';
import { api } from '../services/api'
import '../App.css'



export function UsersForm() {
    const { id } = useParams();
    const inputName = useRef<HTMLInputElement>(null);
    const inputEmail = useRef<HTMLInputElement>(null);
    const inputBirthDate = useRef<HTMLInputElement>(null);

    useEffect(() => {
        if (id) {
            getUserById();
        }
    }, [id]);

    async function getUserById() {
        const response = await api.get(`/users/get-user-by-id/${id}`);

        if (inputName.current)
            inputName.current.value = response.data.name;

        if (inputEmail.current)
            inputEmail.current.value = response.data.email;

        if (inputBirthDate.current)
            inputBirthDate.current.value = response.data.birthDate;
    }

    async function saveUser() {

        let userData = {};
        if (id) {
            userData = {
                Id: Number(id),
                Name: inputName.current?.value,
                Email: inputEmail.current?.value,
                BirthDate: inputBirthDate.current?.value
            };
        } else {
            userData = {
                Name: inputName.current?.value,
                Email: inputEmail.current?.value,
                BirthDate: inputBirthDate.current?.value
            }
        }

        if (id) {
            await api.put(`/users/update-user-by-id/${id}`, userData);
        } else {
            console.log("userdata ", userData);
            await api.post('/users/create-user', userData);
        }

        clearFields();
    }

    function clearFields() {
        if (inputName.current) inputName.current.value = "";
        if (inputEmail.current) inputEmail.current.value = "";
        if (inputBirthDate.current) inputBirthDate.current.value = "";
    }

    return (
        <div className="container-register">
            <form className="center-form">
                <h1>{id ? "Editar Usuário" : "Cadastro de Usuários"}</h1>
                <input name="name" type="text" placeholder="Nome" ref={inputName} />
                <input name="email" type="email" placeholder="Email" ref={inputEmail} />
                <input name="birthdate" type="date" ref={inputBirthDate} />

                <button type="button" onClick={saveUser}>
                    {id ? "Atualizar" : "Registrar"}
                </button>

                <NavButton className="navButtons" to="/" label="Início" />
                <NavButton className="navButtons" to="/users" label="Voltar" />
            </form>
        </div>
    );
}