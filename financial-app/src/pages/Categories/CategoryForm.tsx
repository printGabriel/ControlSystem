import { useRef, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { NavButton } from '../../components/NavButton';
import { api } from '../../services/api'
import '../../App.css'

export function CategoryForm() {
    const { id } = useParams();
    const inputDescription = useRef<HTMLInputElement>(null);
    const inputPurposeType = useRef<HTMLSelectElement>(null);

    //chamada da função para buscar categoria pelo id, caso a tela tenha sido acessada com um identificador.
    useEffect(() => {
        if (id) {
            getCategoryById();
        }
    }, [id]);

    //retorno da função principal, formulário de registro de categoria
    return (
        <div className="container-register">
            <form className="center-form">
                <h1>Cadastro de categorias</h1>
                <input name="description" type="text" placeholder="Descrição:" ref={inputDescription} />
                <select ref={inputPurposeType} defaultValue={""}>
                    <option value="">Escolha a finalidade da categoria:</option>
                    <option value="1">Despesa</option>
                    <option value="2">Receita</option>
                </select>
                {/* aqui ele valida se a tela foi acessada com id ou não e mostra atualizar ou registrar de acordo. */}
                <button type="button" onClick={saveCategory}>
                    {id ? "Atualizar" : "Registrar"}
                </button>

                <NavButton className="navButtons" to="/categories" label="Voltar" />
                <NavButton className="navButtons" to="/" label="Início" />
            </form>
        </div>
    );

    //função para buscar a categoria pelo id, caso a tela tenha sido chamada por edição
    async function getCategoryById() {
        const response = await api.get(`/categories/${id}`);

        if (inputDescription.current)
            inputDescription.current.value = response.data.description;

        if (inputPurposeType.current)
            inputPurposeType.current.value = response.data.purposeType.toString();
    }

    //função utilizada para salvar as ações na tela:
    async function saveCategory() {
        let categoryData = {};
        var create = null;

        //caso o usuário não tenha preenchido algum campo, um alerta é disparado
        if (
            inputDescription.current?.value == ""
            || inputPurposeType.current?.value == ""
        ) {
            alert("Preencha todos os campos!")
            return;
        }

        //condição abaixo segue a mesma ideia da tela de usuários para registrar ou editar uma transação
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
        //caso a tela tenha sido chamada pela edição, registra o que foi alterado,
        // caso tenha sido chamada para novo registro, executa essa ação
        try {
            if (id) {
                await api.put(`/categories/${id}`, categoryData);
            } else {
                create = await api.post('/categories/', categoryData);

            }
        } catch (error: any) {
            alert(error.response.data);
            return;
        }

        //limpa os campos caso a ação seja criação
        if (create != null)
            clearFields();
    }

    function clearFields() {
        if (inputDescription.current) inputDescription.current.value = "";
        if (inputPurposeType.current) inputPurposeType.current.value = "";
    }
}