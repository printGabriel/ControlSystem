
import { useRef } from 'react';
import { NavButton } from '../components/NavButton';
import { api } from '../services/api'
import '../App.css'

export function CategoryForm() {
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputPurposeType = useRef<HTMLInputElement>(null);

    async function createCategory() {
        await api.post('/categories/create-category', {
            Description: inputDescription.current?.value,
            PurposeType: inputPurposeType.current?.value,
        });

        if (inputDescription.current) {
            inputDescription.current.value = "";
        }

        if (inputPurposeType.current) {
            inputPurposeType.current.value = "";
        }
    }

    return (
        <div className="container-register">
            <form className="center-form">
                <h1>Cadastro de categorias</h1>
                <input name="description" type="text" placeholder="Descrição:" ref={inputDescription} />
                <input name="purposeType" type="number" placeholder="Finalidade da categoria:" ref={inputPurposeType} />
                <button type="button" onClick={createCategory}>Registrar</button>
                <NavButton className="navButtons" to="/" label="Home" />
            </form>
        </div>
    );
}