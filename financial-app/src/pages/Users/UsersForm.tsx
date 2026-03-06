import { useRef, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../../components/NavButton';
import { api } from '../../services/api'
import '../../App.css'

// formulário utilizado para adicionar um novo usuário
export function UsersForm() {
    // const para receber o valor de um id caso a tela seja acessada para edição
    const { id } = useParams();
    const inputName = useRef<HTMLInputElement>(null);
    const inputEmail = useRef<HTMLInputElement>(null);
    const inputBirthDate = useRef<HTMLInputElement>(null);

    //chamada do método caso tenha um id na requisição
    useEffect(() => {
        if (id) {
            getUserById();
        }
    }, [id]);

    //retorno do formulário para preenchimento de um novo usuário
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

                {/* botões de navegção para voltar a página anterior ou início */}
                <NavButton className="navButtons" to="/users" label="Voltar" />
                <NavButton className="navButtons" to="/" label="Início" />
            </form>
        </div>
    );

    //função utilizada para puxar via api, os dados do usuário para edição
    async function getUserById() {
        const response = await api.get(`/users/${id}`);

        if (inputName.current)
            inputName.current.value = response.data.name;

        if (inputEmail.current)
            inputEmail.current.value = response.data.email;

        if (inputBirthDate.current)
            inputBirthDate.current.value = response.data.birthDate;
    }

    //função para salvar a ação na tela
    async function saveUser() {
        let userData = {};
        var create = null;
        //caso algum campo não tenha sido preenchido, um alerta é disparado pedindo o preenchimento
        if (
            inputName.current?.value == ""
            || inputEmail.current?.value == ""
            || inputBirthDate.current?.value == ""
        ) {
            alert("Preencha todos os campos!")
            return;
        }

        //tive que fazer essa condição um pouco rápido, não é o ideal, mas...
        //para edição envia Id, para create não envia.
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

        // verifica se há algum erro e altera o usuário caso o parâmetro venha com id ou cria
        //um usuário caso não venha com id
        try {
            if (id) {
                await api.put(`/users/${id}`, userData);
            } else {
                create = await api.post('/users', userData);
            }
        }
        catch (error: any) {
            alert(error.response.data);
            return;
        }

        //caso seja create, zera os campos por meio da função abaixo
        if (create != null)
            clearFields();
    }

    function clearFields() {
        if (inputName.current) inputName.current.value = "";
        if (inputEmail.current) inputEmail.current.value = "";
        if (inputBirthDate.current) inputBirthDate.current.value = "";
    }
}