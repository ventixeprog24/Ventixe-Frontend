const validateField = (field) => {
	let errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`);

	if (!errorSpan) return;

	let errorMessage = "";
	let value = field.value.trim();

	if (field.hasAttribute("data-val-required") && value === "")
		errorMessage = field.getAttribute("data-val-required");

	if (field.hasAttribute("data-val-minlength") && value !== "") {
		let minLength = parseInt(field.getAttribute("data-val-minlength-min"));
		if (value.length < minLength)
			errorMessage = field.getAttribute("data-val-minlength");
	}

	if (field.hasAttribute("data-val-range") && value !== "") {
		let minRange = parseInt(field.getAttribute("data-val-range-min"));
		let inputValue = parseFloat(value);

		if (inputValue < minRange)
			errorMessage = field.getAttribute("data-val-range");
	}

	if (errorMessage) {
		field.classList.add("input-validation-error");
		errorSpan.classList.remove("field-validation-valid");
		errorSpan.classList.add("field-validation-error");
		errorSpan.textContent = errorMessage;
	} else {
		field.classList.remove("input-validation-error");
		errorSpan.classList.remove("field-validation-error");
		errorSpan.classList.add("field-validation-valid");
		errorSpan.textContent = "";
	}
};

document.addEventListener("DOMContentLoaded", function () {
	const form = document.querySelector("form");
	if (!form) return;

	const fields = document.querySelectorAll("input[data-val='true']")

	fields.forEach(field => {
		field.addEventListener("input", function () {
			validateField(field);
		});
	});
});