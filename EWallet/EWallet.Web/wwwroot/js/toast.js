function showToast(message, success) {
    let container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        container.className = 'toast-container position-fixed top-0 start-50 translate-middle-x p-3';
        container.style.zIndex = 2000;
        document.body.appendChild(container);
    }

    const toastEl = document.createElement('div');
    toastEl.className = 'toast align-items-center border-0 text-bg-' + (success ? 'success' : 'danger');
    toastEl.setAttribute('role', success ? 'status' : 'alert');
    toastEl.setAttribute('aria-live', success ? 'polite' : 'assertive');
    toastEl.setAttribute('aria-atomic', 'true');

    const wrapper = document.createElement('div');
    wrapper.className = 'd-flex';

    const body = document.createElement('div');
    body.className = 'toast-body';
    body.textContent = message;

    const closeBtn = document.createElement('button');
    closeBtn.type = 'button';
    closeBtn.className = 'btn-close btn-close-white me-2 m-auto';
    closeBtn.setAttribute('data-bs-dismiss', 'toast');
    closeBtn.setAttribute('aria-label', 'Close');

    wrapper.appendChild(body);
    wrapper.appendChild(closeBtn);
    toastEl.appendChild(wrapper);
    container.appendChild(toastEl);

    const toast = new bootstrap.Toast(toastEl, { delay: 3500 });
    toastEl.addEventListener('hidden.bs.toast', () => toastEl.remove());
    toast.show();
}

async function submitAjax(form, onSuccess) {
    try {
        const response = await fetch(form.action, {
            method: 'POST',
            body: new FormData(form),
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        });
        const result = await response.json();
        showToast(result.message, result.success);

        if (result.success) {
            if (result.newBalance !== null && result.newBalance !== undefined) {
                const el = document.getElementById('user-balance');
                if (el) el.textContent = result.newBalance;
            }
            if (onSuccess) onSuccess(result);
        }
    } catch (e) {
        showToast(window.appMessages.connectionError, false);
    }
}