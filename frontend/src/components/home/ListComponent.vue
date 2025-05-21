<template>
    <div class="flex p-4 justify-between">
        <div class="flex gap-4">
            <form @submit.prevent="search" class="flex items-center max-w-sm mx-auto">   
                <label for="simple-search" class="sr-only">Search</label>
                <div class="relative w-full">
                    <input v-model="querySearch" type="text" id="simple-search" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full ps-5 p-2.5" placeholder="Search..." required />
                </div>
                <button type="submit" class="p-2.5 ms-2 text-sm font-medium text-white bg-blue-700 rounded-lg border border-blue-700 hover:bg-blue-800">
                    <svg class="w-4 h-4" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"/>
                    </svg>
                    <span class="sr-only">Search</span>
                </button>
            </form>
            <select @change="sort" v-model="sortOrder" class="border border-gray-300 rounded-md px-3 py-2 text-sm text-gray-700 bg-white focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500">
                <option value="asc">ASC</option>
                <option value="desc">DESC</option>
            </select>
        </div>
        <CreateListComponent ref="updatedList" @list="lists" />
    </div>
    <div class="font-sans overflow-x-auto p-2">
        <table class="min-w-full bg-white">
            <thead class="bg-blue-600 whitespace-nowrap">
                <tr>
                    <th class="px-4 py-3 text-left text-sm font-semibold text-white border">No.</th>
                    <th class="px-4 py-3 text-left text-sm font-semibold text-white border">Tittle</th>
                    <th class="px-4 py-3 text-left text-sm font-semibold text-white border">Content</th>
                    <th class="px-4 py-3 text-left text-sm font-semibold text-white border">Created At</th>
                    <th class="px-4 py-3 text-left text-sm font-semibold text-white border">Updated At</th>
                    <th class="px-4 py-3 text-left text-sm font-semibold text-white border">Actions</th>
                </tr>
            </thead>
            <tbody class="whitespace-nowrap">
                <tr v-for="(item, index) in items" :key="index" class="hover:bg-blue-50 even:bg-gray-100">
                    <td class="px-4 py-3 text-[15px] text-gray-800 font-semibold border">{{ item.id }}</td>
                    <td class="px-4 py-3 text-[15px] text-gray-800 border">{{ item.title }}</td>
                    <td class="px-4 py-3 text-[15px] text-gray-800 border">{{ item.content }}</td>
                    <td class="px-4 py-3 text-[15px] text-gray-800 border">{{ formatDate(item.createdAt) }}</td>
                    <td class="px-4 py-3 text-[15px] text-gray-800 border">{{ formatDate(item.updatedAt) }}</td>
                    <td class="px-4 py-3 text-[15px] text-gray-800 border flex gap-3">
                        <div @click="updated(item)" class="cursor-pointer">
                            <svg xmlns="http://www.w3.org/2000/svg" class="w-5 fill-gray-500" viewBox="0 0 348.882 348.882">
                                <path
                                    d="m333.988 11.758-.42-.383A43.363 43.363 0 0 0 304.258 0a43.579 43.579 0 0 0-32.104 14.153L116.803 184.231a14.993 14.993 0 0 0-3.154 5.37l-18.267 54.762c-2.112 6.331-1.052 13.333 2.835 18.729 3.918 5.438 10.23 8.685 16.886 8.685h.001c2.879 0 5.693-.592 8.362-1.76l52.89-23.138a14.985 14.985 0 0 0 5.063-3.626L336.771 73.176c16.166-17.697 14.919-45.247-2.783-61.418zM130.381 234.247l10.719-32.134.904-.99 20.316 18.556-.904.99-31.035 13.578zm184.24-181.304L182.553 197.53l-20.316-18.556L294.305 34.386c2.583-2.828 6.118-4.386 9.954-4.386 3.365 0 6.588 1.252 9.082 3.53l.419.383c5.484 5.009 5.87 13.546.861 19.03z"
                                    data-original="#000000" />
                                <path
                                    d="M303.85 138.388c-8.284 0-15 6.716-15 15v127.347c0 21.034-17.113 38.147-38.147 38.147H68.904c-21.035 0-38.147-17.113-38.147-38.147V100.413c0-21.034 17.113-38.147 38.147-38.147h131.587c8.284 0 15-6.716 15-15s-6.716-15-15-15H68.904C31.327 32.266.757 62.837.757 100.413v180.321c0 37.576 30.571 68.147 68.147 68.147h181.798c37.576 0 68.147-30.571 68.147-68.147V153.388c.001-8.284-6.715-15-14.999-15z"
                                    data-original="#000000" />
                            </svg>
                        </div>
                        <div @click="deleted(item.id)" class="cursor-pointer">
                            <svg xmlns="http://www.w3.org/2000/svg" class="w-5 fill-gray-500" viewBox="0 0 24 24">
                            <path
                                d="M19 7a1 1 0 0 0-1 1v11.191A1.92 1.92 0 0 1 15.99 21H8.01A1.92 1.92 0 0 1 6 19.191V8a1 1 0 0 0-2 0v11.191A3.918 3.918 0 0 0 8.01 23h7.98A3.918 3.918 0 0 0 20 19.191V8a1 1 0 0 0-1-1Zm1-3h-4V2a1 1 0 0 0-1-1H9a1 1 0 0 0-1 1v2H4a1 1 0 0 0 0 2h16a1 1 0 0 0 0-2ZM10 4V3h4v1Z"
                                data-original="#000000" />
                            <path d="M11 17v-7a1 1 0 0 0-2 0v7a1 1 0 0 0 2 0Zm4 0v-7a1 1 0 0 0-2 0v7a1 1 0 0 0 2 0Z"
                                data-original="#000000" />
                            </svg>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div ref="modalEl" id="default-modal" tabindex="-1" aria-hidden="true" class="hidden fixed top-0 left-0 right-0 z-50 w-full p-4 overflow-x-hidden overflow-y-auto max-h-full">
        <div class="relative w-full max-w-2xl max-h-full">
            <div class="relative bg-white rounded-lg shadow">
                <div class="p-4 border-b">
                    <h3 class="text-xl font-semibold text-gray-900">Confirm</h3>
                    <button @click="hideModal" type="button" class="absolute top-3 right-2.5 text-gray-400 hover:text-gray-900">✕</button>
                </div>
                <div class="border-b p-4">
                    <h1>Do you want to deleted ?</h1>
                </div>
                <div class="p-2 space-y-4 md:space-y-6">
                    <div class="flex gap-4 items-center justify-center">
                        <button @click="confirmDeleted" type="submit" class="w-[10%] text-white bg-blue-600 hover:bg-blue-700 font-medium rounded-lg text-sm px-5 py-2.5 text-center">Yes</button>
                        <button @click="cancel" type="submit" class="w-[10%] text-white bg-red-600 hover:bg-red-700 font-medium rounded-lg text-sm px-5 py-2.5 text-center">No</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script setup lang="ts">
    import CreateListComponent from './CreateListComponent.vue';
    import { ref, onMounted } from 'vue';
    import { Modal } from 'flowbite';
    import dayjs from 'dayjs';
    import { useStore } from 'vuex';
    import type { TTest } from '../../types/index';
    import { ToastifySuccess, ToastifyError } from '../../utils/Toastify';

    const store = useStore();
    let modalInstance: Modal | null = null;
    const items = ref<TTest[]>([]);
    const updatedList = ref();
    const modalEl = ref<HTMLElement | null>(null);
    const Id = ref<string>('');
    const querySearch = ref<string>('');
    const sortOrder = ref<string>('desc');

    onMounted(() => {
        lists();
        if (modalEl.value) {
            modalInstance = new Modal(modalEl.value);
        }
    });
    const showModal = () => modalInstance?.show();
    const hideModal = () => modalInstance?.hide();

    const formatDate = (date: string) => {
        return dayjs(date).format('YYYY-MM-DD HH:mm');
    };
    const lists = async () => {
        try {
            const response = await store.dispatch('test/lists', { vuex: true })
            if (response.status === 200) {
                items.value = response.data.data;
            }
        } catch (err) {
            console.error(err);
        }
    };
    const updated = (item: TTest) => {
        updatedList.value?.openModalForEdit(item);
    };
    const cancel = () => modalInstance?.hide();

    const deleted = async (id: string) => {
        Id.value = id;
        showModal();
    };
    const confirmDeleted = async () => {
        try {
            const response = await store.dispatch('test/deleted', { id: Id.value });
            if (response.status === 200) {
                ToastifySuccess('List delete successful');
                hideModal();
                lists();
            }
        } catch (err) {
            console.error(err);
            ToastifyError('List fail delete, Please try again!');
        }
    };
    const search = async () => {
        try {
            const response = await store.dispatch('test/search', { id: querySearch.value });
            if (response.status === 200) {
                items.value = response.data.data;
            };
        } catch (err) {
            console.error(err);
            ToastifyError('Id not found, Please try again!');
        }
    };
    const sort = () => {
        if (!sortOrder.value) return;
        items.value.sort((a, b) => {
            const dateA = new Date(a.createdAt).getTime();
            const dateB = new Date(b.createdAt).getTime();
            return sortOrder.value === 'asc' ? dateA - dateB : dateB - dateA;
        });
    };

</script>