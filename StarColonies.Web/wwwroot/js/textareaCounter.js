

const textInput = document.querySelector("#textarea");
const counter = document.querySelector("#charCounter");

if (textInput && counter) {
    const updateCount = () => {
        let value = textInput.value;

        if (value.length > 1000) {
            textInput.value = value.substring(0, 1000);
            value = textInput.value;
        }

        counter.textContent = `${value.length} / 1000`;
    };

    textInput.addEventListener("input", updateCount);
    updateCount();
}