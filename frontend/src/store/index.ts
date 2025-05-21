import { createStore } from 'vuex';
import { test } from './modules/test';
import { login } from './modules/login';
import { register } from './modules/register';

export const store = createStore({
    modules: {
        test,
        login,
        register
    },
});
