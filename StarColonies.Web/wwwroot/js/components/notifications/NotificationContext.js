
export class NotificationContext {
    constructor(strategy) {
        this.strategy = strategy;
    }
    
    setStrategy(strategy) {
        this.strategy = strategy;
    }
    
    success(message) {
        this.strategy.notifySuccess(message);
    }
    
    error(message) {
        this.strategy.notifyError(message);
    }
}