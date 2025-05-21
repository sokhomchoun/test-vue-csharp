<template>
    <button @click="showModal" class="bg-blue-600 text-white py-1.5 px-6 rounded text-[15-px]">
        Add New
    </button>
    <div ref="modalEl" id="default-modal" tabindex="-1" aria-hidden="true" class="hidden fixed top-0 left-0 right-0 z-50 w-full p-4 overflow-x-hidden overflow-y-auto max-h-full">
        <div class="relative w-full max-w-2xl max-h-full">
            <div class="relative bg-white rounded-lg shadow">
                <div class="p-4 border-b">
                    <h3 class="text-xl font-semibold text-gray-900">Add New</h3>
                    <button @click="hideModal" type="button" class="absolute top-3 right-2.5 text-gray-400 hover:text-gray-900">✕</button>
                </div>
                <div class="p-4 space-y-4 md:space-y-6">
                    <div>
                        <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-black">Tittle</label>
                        <input v-model="title" type="text" class="bg-gray-50 border border-gray-300 rounded-lg block w-full p-2.5">
                    </div>
                    <div>
                        <label class="block mb-2 text-sm font-medium">Content</label>
                        <input v-model="content" type="text" class="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg block w-full p-2.5">
                    </div>
                    <div class="flex gap-4 items-center justify-center">
                        <button @click="save" type="submit" class="w-[20%] text-white bg-blue-600 hover:bg-blue-700 font-medium rounded-lg text-sm px-5 py-2.5 text-center">Save</button>
                        <button @click="cancel" type="submit" class="w-[20%] text-white bg-red-600 hover:bg-red-700 font-medium rounded-lg text-sm px-5 py-2.5 text-center">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
    import { ref, onMounted, defineEmits } from 'vue';
    import { Modal } from 'flowbite';
    import { useStore } from 'vuex';
    import { ToastifySuccess, ToastifyError } from '../../utils/Toastify';
    import type { TTest } from '../../types';

    const emit = defineEmits(['list']);
    const modalEl = ref<HTMLElement | null>(null);
    let modalInstance: Modal | null = null;
    const store = useStore();
    const isEditMode = ref(false);
    const editItemId = ref<string | null>(null);
    const title = ref('');
    const content = ref('');

    onMounted(() => {
        if (modalEl.value) {
            modalInstance = new Modal(modalEl.value);
        }
    });
    const showModal = () => modalInstance?.show();
    const hideModal = () => modalInstance?.hide();

    const cancel = () => {
        hideModal();
        title.value = '',
        content.value = ''
    }
    const save = async () => {
        try {
            modalInstance?.hide();
            const payload: { title: string; content: string; id?: string | null } = { 
                title: title.value,
                content: content.value,
            };
            if (isEditMode.value && editItemId.value) {
                payload['id'] = editItemId.value;
                await store.dispatch('test/updated', payload);
            } else {
                await store.dispatch('test/save', payload);
            }
            await store.dispatch('test/lists', { vuex: true });
            isEditMode.value ? ToastifySuccess('List update successful') : ToastifySuccess('List create successful');
            title.value = '';
            content.value = '';
            editItemId.value = null;
            isEditMode.value = false;
            emit('list');
            
        } catch (err) {
            console.error(err);
            ToastifyError(
                isEditMode.value ? 'List update failed, please try again!' : 'List create failed, please try again!'
            );
        }
    };
    const openModalForEdit = (item: TTest) => {
        title.value = item.title;
        content.value = item.content;
        editItemId.value = item.id;
        isEditMode.value = true;
        showModal();
    };
    defineExpose({ openModalForEdit });

</script>
