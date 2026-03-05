
import { useEffect, useRef, useState } from 'react';
import { api } from '../../services/api'
import { useNavigate } from 'react-router-dom';
import '../../App.css'
interface User {
    id: number;
    name: string;
    email: string;
    birthDate: string;
}

export function Users() {
    const [users, setUsers] = useState<User[]>([])
    const navigate = useNavigate();
    
    useEffect(() => {
        getUsers()
    }, [])

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
                <button className="buttonAdd" type="button" onClick={() => navi(2)}>Adicionar usuário</button>
                <button className="buttonReturn" type="button" onClick={() => navi(1)}>Início</button>
            </div>
        </div>
    );

    async function getUsers() {
        const usersApi = await api.get('/users/get-all-users')

        setUsers(usersApi.data);
    }

    async function deleteUser(id: number) {
        await api.delete(`/users/delete-user-by-${id}`);
        setUsers(users.filter(user => user.id !== id));
    }

    function editUser(id: number) {
        navigate(`/users/${id}`);
    }

    function navi(type: number) {

        switch (type) {
            case 1: navigate(`/`); break;
            case 2: navigate(`/usersForm/`); break;
        }
    }
}