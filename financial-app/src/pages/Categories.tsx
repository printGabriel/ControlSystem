
import { useEffect, useRef, useState } from 'react';
import { api } from '../services/api'
import { useNavigate } from 'react-router-dom';
import '../App.css'

interface Category {
    id: number;
    description: string;
    purposeType: number;
}

export function Categories() {
    const [categories, setCategories] = useState<Category[]>([])

    async function getCategories() {
        const categoriesApi = await api.get('/categories/get-all-categories')

        setCategories(categoriesApi.data);
    }

    async function deleteCategory(id: number) {
        await api.delete(`/categories/${id}`);
        setCategories(categories.filter(category => category.id !== id));
    }

    const navigate = useNavigate();

    function editCategory(id: number) {
        navigate(`/categoriesForm/${id}`);
    }

    function navi(type: number) {

        switch (type) {
            case 1: navigate(`/`); break;
            case 2: navigate(`/categoriesForm/`); break;
        }
    }

    useEffect(() => {
        getCategories()
    }, [])

    return ( 
        <div className="container-register">
            <div>
                <button style={{ marginRight: "20px", marginBottom: "10px" }} type="button" onClick={() => navi(2)}>Adicionar categoria</button>
                <button style={{ marginRight: "226px", marginBottom: "10px", width: "200px" }} type="button" onClick={() => navi(1)}>Início</button>
            </div>
            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Id:</th>
                            <th>Descrição:</th>
                            <th>Finalidade:</th>
                        </tr>
                    </thead>
                    <tbody>
                        {categories.map((category) => (
                            <tr key={category.id}>
                                <td>{category.id}</td>
                                <td>{category.description}</td>
                                <td>{category.purposeType}</td>
                                <td>
                                    <button onClick={() => editCategory(category.id)}>
                                        Editar
                                    </button>
                                </td>
                                <td>
                                    <button className="deleteButton" onClick={() => deleteCategory(category.id)}>
                                        Deletar
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}