import Toastify from 'toastify-js';

export const ToastifySuccess = (message: string) => {
    Toastify({
        text: message,
        duration: 3000,
        close: true,
        gravity: "top",
        position: "right",
        backgroundColor: "green",
    }).showToast();
};

export const ToastifyError = (message: string) => {
    Toastify({
        text: message,
        duration: 3000,
        close: true,
        gravity: "top",
        position: "right",
        backgroundColor: "red",
    }).showToast();
};
