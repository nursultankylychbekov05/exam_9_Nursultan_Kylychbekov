function showToast(message, success) {
    let container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        document.body.appendChild(container);
    }

    const toast = document.createElement('div');
    toast.className = 'app-toast ' + (success ? 'app-toast-success' : 'app-toast-error');
    toast.textContent = message;
    container.appendChild(toast);

    setTimeout(() => toast.classList.add('app-toast-hide'), 3000);
    setTimeout(() => toast.remove(), 3500);
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
        showToast('Ошибка соединения', false);
    }
}