import { NotificationContext } from './components/notifications/NotificationContext.js';
import { NotyfStrategy } from './components/notifications/NotyStrategy.js';

document.addEventListener("DOMContentLoaded", () => {
    const notificationContext = new NotificationContext(new NotyfStrategy());


    const validationErrors = document.querySelectorAll(".text-danger");
    validationErrors.forEach(errorElement => {
        const errorMessage = errorElement.textContent.trim();
        if (errorMessage.length > 0) {
            notificationContext.error(errorMessage);
        }
    });

    const validationSummary = document.querySelector("[asp-validation-summary='ModelOnly']");
    if (validationSummary) {
        const summaryMessages = validationSummary.querySelectorAll("li");
        summaryMessages.forEach(li => {
            const message = li.textContent.trim();
            if (message.length > 0) {
                notificationContext.error(message);
            }
        });
    }
});
