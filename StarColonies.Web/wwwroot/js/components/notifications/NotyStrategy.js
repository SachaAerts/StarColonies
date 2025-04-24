export class NotyfStrategy {
    constructor() {
        this.notyf = new window.Notyf({
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