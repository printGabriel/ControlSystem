import { useRef, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../components/NavButton';
import { api } from '../services/api'
import '../App.css'

export function CategoryForm() {
    const { id } = useParams();
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputPurposeType = useRef<HTMLInputElement>(null);

    useEffect(() => {
        if (id) {
            getCategoryById();
        }
    }, [id]);

    async function getCategoryById() {
        const response = await api.get(`/categories/get-category-by-id/${id}`);

        if (inputDescription.current)
            inputDescription.current.value = response.data.description;

        if (inputPurposeType.current)
            inputPurposeType.current.value = response.data.purposeType;
    }

    async function saveCategory() {
        let categoryData = {};

        if (id) {
            categoryData = {
                Id: Number(id),
                Description: inputDescription.current?.value,
                PurposeType: inputPurposeType.current?.value
            };
        } else {
            categoryData = {
                Description: inputDescription.current?.value,
                PurposeType: inputPurposeType.current?.value
            }
        }

        if (id) {
            await api.put(`/categories/update-category-by-id/${id}`, categoryData);
        } else {
            await api.post('/categories/create-category', categoryData);
        }

        clearFields();
    }

    function clearFields() {
        if (inputDescription.current) inputDescription.current.value = "";
        if (inputPurposeType.current) inputPurposeType.current.value = "";
    }

    return (
        <div className="container-register">
            <form className="center-form">
                <h1>Cadastro de categorias</h1>
                <input name="description" type="text" placeholder="Descrição:" ref={inputDescription} />
                <input name="purposeType" type="number" placeholder="Finalidade da categoria:" ref={inputPurposeType} />
     
                <button type="button" onClick={saveCategory}>
                    {id ? "Atualizar" : "Registrar"}
                </button>

                <NavButton className="navButtons" to="/categories" label="Voltar" />
                <NavButton className="navButtons" to="/" label="Início" />
            </form>
        </div>
    );
}