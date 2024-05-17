<template>
    <div class="list-container">
        <h2>Lista de Registros</h2>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Author Id</th>
                    <th>Legal Entity Id</th>
                    <th>Describe</th>
                    <th>created Process</th>
                    <th>Updated Process</th>
                    <th>Ações</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="record in records.data" :key="record.id">
                    <td>{{ record.idProcess }}</td>
                    <td>{{ record.author }}</td>
                    <td>{{ record.legalEntity }}</td>
                    <td>{{ record.describeLegalEntity }}</td>
                    <td>{{ record.createdProcess }}</td>
                    <td>{{ record.updatedProcess }}</td>
                    <td>
                        <button @click="editRecord(record)">Editar</button>
                        <button @click="deleteRecord(record.id)">Excluir</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const records = ref([]);
const apiBaseUrl = import.meta.env.VITE_API_URL;

const fetchRecords = async () => {
    try {
        const response = await axios.get(`${apiBaseUrl}/LegalContracts`);
        records.value = response.data;
    } catch (error) {
        console.error('Erro ao buscar registros:', error);
    }
};

const editRecord = (record) => {
    // Implementar lógica de edição
    console.log('Editando registro:', record);
};

const deleteRecord = async (id) => {
    try {
        await axios.delete(`${apiBaseUrl}/LegalContracts/${id}`);
        records.value = records.value.filter(record => record.id !== id);
    } catch (error) {
        console.error('Erro ao excluir registro:', error);
    }
};

onMounted(() => {
    fetchRecords();
});
</script>

<style scoped>
.list-container {
    margin-top: 20px;
    background-color: white;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

h2 {
    margin-bottom: 20px;
}

table {
    width: 100%;
    border-collapse: collapse;
}

th,
td {
    padding: 10px;
    border: 1px solid #ccc;
}

th {
    background-color: #f4f4f4;
}

button {
    padding: 5px 10px;
    margin-right: 5px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

button:hover {
    opacity: 0.8;
}
</style>
