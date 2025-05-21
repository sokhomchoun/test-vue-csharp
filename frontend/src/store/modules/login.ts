import axios from "../../config/config"

export const login = {
    namespaced: true,
    getters: {

    },
    actions: {
        signin: function (context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = "User/login";
                axios.post(url,payload).then((res) => {
                    resolve(res);
                }).catch((err) => {
                    reject(err);
                });
            });
        },
    }

}