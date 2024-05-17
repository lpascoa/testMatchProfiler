<template>
    <div class="form-container">
        <h2>Editar Registro</h2>
        <form @submit.prevent="handleSubmit">
            <div>
                <label for="idProcesso">Id Process 1:</label>
                <input type="text" v-model="form.idProcesso" id="idProcesso" required />
            </div>
            <div>
                <label for="describeProcess">describe Process 2:</label>
                <input type="text" v-model="form.describeProcess" id="describeProcess" required />
            </div>
            <div>
                <label for="cboAuthor">Author:</label>
                <select v-model="form.author" id="cboAuthor">
                    <option v-for="option in options1.data" :key="option.idAuthor" :value="option.describe">
                        {{ option.text }}
                    </option>
                </select>
            </div>
            <div>
                <label for="cboLegalEntity">Combo 2:</label>
                <select v-model="form.legalEntity" id="cboLegalEntity">
                    <option v-for="option in options2" :key="option.idLegalEntity" :value="option.describe">
                        {{ option.text }}
                    </option>
                </select>
            </div>
            <button type="submit">Salvar</button>
            <button @click="cancelEdit">Cancelar</button>
        </form>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';

const form = ref({
    idProcesso: '',
    describeProcess: '',
    author: '',
    legalEntity: ''
});

const options1 = ref([]);
const options2 = ref([]);

const route = useRoute();
const router = useRouter();
const apiBaseUrl = process.env.VITE_API_URL;

const fetchOptions = async () => {
    try {
        const response1 = await axios.get(`${apiBaseUrl}/Author`);
        options1.value = response1.data;

        const response2 = await axios.get(`${apiBaseUrl}/LegalEntity`);
        options2.value = response2.data;
    } catch (error) {
        console.error('Erro ao buscar opções:', error);
    }
};

const fetchRecord = async () => {
    try {
        const response = await axios.get(`${apiBaseUrl}/records/${route.params.id}`);
        form.value = response.data;
    } catch (error) {
        console.error('Erro ao buscar registro:', error);
    }
};

const handleSubmit = async () => {
    try {
        await axios.put(`${apiBaseUrl}/records/${route.params.id}`, form.value);
        alert('Registro atualizado com sucesso!');
        router.push('/');
    } catch (error) {
        console.error('Erro ao atualizar registro:', error);
    }
};

const cancelEdit = () => {
    router.push('/');
};

onMounted(() => {
    fetchOptions();
    fetchRecord();
});
</script>

<style scoped>
.form-container {
    background-color: white;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

h2 {
    margin-bottom: 20px;
}

form div {
    margin-bottom: 10px;
}

label {
    display: block;
    margin-bottom: 5px;
}

input,
select,
button {
    width: 100%;
    padding: 8px;
    margin-bottom: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
}

button {
    background-color: #4CAF50;
    color: white;
    cursor: pointer;
    border: none;
}

button:hover {
    background-color: #45a049;
}
</style>
