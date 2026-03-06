
import { useEffect, useRef, useState } from 'react';
import { api } from '../../services/api'
import { useNavigate } from 'react-router-dom';
import '../../App.css'

//interface para tipar as propriedades do objeto vindo do banco
interface User {
    id: number;
    name: string;
    email: string;
    birthDate: string;
}
//função utilizada para listar todos os usuários em uma tabela
export function Users() {
    const [users, setUsers] = useState<User[]>([])
    const navigate = useNavigate();

    //chamada do método getUsers
    useEffect(() => {
        getUsers()
    }, [])

    //retorno da função principal, a tabela com usuários, opção de excluir e chamada para a tela de edição
    return (
        <div >
            <div className="container-register">
                <table>
                    <thead>
                        <tr>
                            <th>Id:</th>
                            <th>Nome:</th>
                            <th>E-mail:</th>
                            <th>Data de nascimento</th>
                            <th>Editar</th>
                            <th>Excluir</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map((user) => (
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.name}</td>
                                <td>{user.email}</td>
                                {/* formatação para a data ficar em pt-Br */}
                                <td>{new Date(user.birthDate).toLocaleDateString('pt-BR')}</td>
                                <td>
                                    <button onClick={() => editUser(user.id)}>
                                        Editar
                                    </button>
                                </td>
                                <td>
                                    <button className="deleteButton" onClick={() => deleteUser(user.id)}>
                                        Deletar
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <div>
                {/* botão para adicionar novo usuário e o debaixo para voltar para o início */}
                <button className="buttonAdd" type="button" onClick={() => navi(2)}>Adicionar usuário</button>
                <button className="buttonReturn" type="button" onClick={() => navi(1)}>Início</button>
            </div>
        </div>
    );

    //função para obter todos os usuários e popular a tabela
    async function getUsers() {
        const usersApi = await api.get('/users/')

        setUsers(usersApi.data);
    }

    //função para deletar o usuário da tabela (deleta suas respectivas transações também)
    async function deleteUser(id: number) {
        await api.delete(`/users/${id}`);
        setUsers(users.filter(user => user.id !== id));
    }

    //função utilizada para acessar o formulário de edição de um usuário, passando seu id 
    function editUser(id: number) {
        navigate(`/users/${id}`);
    }

    //pequena função com switch case pra navegar para o início ou formulário
    function navi(type: number) {
        switch (type) {
            case 1: navigate(`/`); break;
            case 2: navigate(`/usersForm/`); break;
        }
    }
}