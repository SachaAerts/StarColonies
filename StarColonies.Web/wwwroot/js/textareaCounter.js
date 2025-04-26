

const textInput = document.querySelector("#textarea");
const counter = document.querySelector("#charCounter");

if (textInput && counter) {
    const updateCount = () => {
        let value = textInput.value;

        if (value.length > 500) {
            textInput.value = value.substring(0, 500);
            value = textInput.value;
        }

        counter.textContent = `${value.length} / 500`;
    };

    textInput.addEventListener("input", updateCount);
    updateCount();
}