// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', () => {
    const MODAL_OPTIONS = {};
    let modals = document.querySelectorAll('.modal');
    let modalInstances = M.Modal.init(modals, MODAL_OPTIONS);
    const SELECT_FORMS_OPTIONS = {
        dropdownOptions: {
            container: document.body
        }
    };
    let noDefault = document.querySelectorAll('select.no-default');
    noDefault.forEach((element) => {
        element.selectedIndex = -1;
    });
    let selects = document.querySelectorAll('select:not(.select2)');
    let selectInstances = M.FormSelect.init(selects, SELECT_FORMS_OPTIONS);
    let gameTypeSelect = document.getElementById('Game_Type');
    if (gameTypeSelect) {
        gameTypeSelect.addEventListener('change', function (event) {
            let label = this.options[this.selectedIndex].text;
            let gameHeroSelect = document.getElementById('Game_Heroes');
            if (!gameHeroSelect)
                return;
            let optionsToActivate = gameHeroSelect.querySelector(`optgroup[label=${label}]`);
            if (optionsToActivate) {
                gameHeroSelect.querySelectorAll('optgroup option').forEach(function (element) {
                    element.disabled = true;
                    element.selected = false;
                });
                optionsToActivate.querySelectorAll('option').forEach(function (element) {
                    element.disabled = false;
                });
                gameHeroSelect.dispatchEvent(new Event('change'));
            }
            else {
                gameHeroSelect.querySelectorAll('option').forEach(function (element) {
                    element.disabled = false;
                });
            }
            M.FormSelect.getInstance(gameHeroSelect).destroy();
            M.FormSelect.init(gameHeroSelect, SELECT_FORMS_OPTIONS);
        });
    }
});
//# sourceMappingURL=site.js.map