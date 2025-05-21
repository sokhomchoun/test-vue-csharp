<script setup lang="ts">
import { ref, provide } from 'vue';

    const token = ref<string | null>(localStorage.getItem('authToken'));

    const setToken = (newToken: string) => {
        token.value = newToken;
        sessionStorage.setItem('authToken', newToken);
    };

    const clearToken = () => {
        token.value = null;
        sessionStorage.removeItem('authToken');
    };

    provide('authToken', token);
    provide('setAuthToken', setToken);
    provide('clearAuthToken', clearToken);
</script>

<template>
    <slot />
</template>
