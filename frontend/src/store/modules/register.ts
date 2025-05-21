import axios from '../../config/config';

export const register = {
    namespaced: true,
    actions: {
        signup: function (context: any, payload: any) {
            return new Promise((resolve, reject) => {
                let url = "User";
                axios.post(url,payload).then((res) => {
                    resolve(res);
                }).catch((err) => {
                    reject(err);
                });
            });
        },
    }
}