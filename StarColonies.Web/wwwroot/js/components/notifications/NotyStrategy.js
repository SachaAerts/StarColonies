import { Notyf } from 'https://cdn.jsdelivr.net/npm/notyf@3/notyf.es.min.js';

export class NotyfStrategy {
    constructor() {
        this.notyf = new Notyf({
            duration: 8000,
            ripple: true,
            position: { x: 'right', y: 'top' }
        });
    }

    notifySuccess(message) {
        this.notyf.success(message);
    }

    notifyError(message) {
        this.notyf.error(message);
    }
}