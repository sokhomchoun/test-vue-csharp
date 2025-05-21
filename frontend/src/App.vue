<template>
    <AuthProvider>
        <router-view />
    </AuthProvider>
</template>

<script setup lang='ts'>
    import AuthProvider from './contexts/AuthProvider.vue';
</script>
