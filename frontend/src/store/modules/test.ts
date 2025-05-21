import axios from '../../config/config';

export const test = {
    namespaced: true,
    state: {
        lists: [],
    },
    getters: {
        lists: function (state: any) {
            return state.lists;
        },
    },
    actions: {
        lists: function(context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = 'Test';
                axios.get(url).then((res) => {
                    if(typeof payload.vuex === "undefined" || payload.vuex === true) {
                        context.commit('lists', res.data.data);
                    }
                    resolve(res);
                }).catch((err) => {
                    reject(err);
                });
            });
        },
        save: function (context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = "Test";
                axios.post(url,payload).then((res) => {
                    context.commit('lists', res.data.data);
                    resolve(res);
                }).catch((err) => {
                    reject(err);
                });
            });
        },
        updated: function (context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = `Test/${payload.id}`;
                axios.put(url, payload).then((res) => {
                    context.commit('lists', res.data.data);
                    resolve(res);
                }).catch((err) => {
                    reject(err);
                })
            })
        },
        deleted: function (context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = `Test/${payload.id}`;
                axios.delete(url, { data: payload }).then((res) => {
                    context.commit('lists', res.data.data);
                    resolve(res);
                })
                .catch((err) => {
                    reject(err);
                });
            });
        },
        search: function (context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = 'Test/search';
                axios.get(url, { params: payload }).then((res) => {
                    context.commit('lists', res.data.data);
                    resolve(res);
                })
                .catch((err) => {
                    reject(err);
                });
            });
        }
        
    },
    mutations: {
        lists: function (state: any, payload: any) {
            state.lists = payload
        },
    }

}