document.addEventListener('DOMContentLoaded', (event) => {
    const forms = document.querySelectorAll('.needs-validation');
    Array.prototype.slice.call(forms)
        .forEach(form => {
            form.addEventListener('submit', event => {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    const LOCAL_STORAGE_KEY = "toggle-bootstrap-theme";
    const LOCAL_META_DATA = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY));
    const DARK_THEME_PATH = "https://cdn.jsdelivr.net/npm/bootswatch@4.5.2/dist/cyborg/bootstrap.min.css";
    const DARK_STYLE_LINK = document.getElementById("dark-theme-style");
    const THEME_TOGGLER = document.getElementById("theme-toggler");

    let isDark = LOCAL_META_DATA && LOCAL_META_DATA.isDark;
    if (isDark) {
        enableDarkTheme();
    } else {
        disableDarkTheme();
    }

    THEME_TOGGLER.addEventListener('click', event => {
        isDark = !isDark;
        if (isDark) {
            enableDarkTheme();
        } else {
            disableDarkTheme();
        }
        localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify({ isDark }));
    });

    function enableDarkTheme() {
        DARK_STYLE_LINK.setAttribute("href", DARK_THEME_PATH);
        THEME_TOGGLER.innerHTML = "🌙";
    }

    function disableDarkTheme() {
        DARK_STYLE_LINK.setAttribute("href", "");
        THEME_TOGGLER.innerHTML = "🌞";
    }
});
