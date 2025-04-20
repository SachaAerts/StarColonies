
export function waitForElement(selector, callback, retry = 5, delay = 50) {
    const element = document.querySelector(selector);
    if (element) {
        callback(element);
    } else if (retry > 0) {
        setTimeout(() => waitForElement(selector, callback, retry - 1, delay), delay);
    }
}
