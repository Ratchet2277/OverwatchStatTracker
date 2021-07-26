// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', () => {
    const SELECT_FORMS_OPTIONS: Partial<M.FormSelectOptions> = {
        dropdownOptions: {
            container: document.body
        }
    }
    
    let AddGame: Vue = new Vue({
        el: "#add-game",
        data: {
            maps: [],
            heroes: [],
            roles: [],
            selectedRole: -1,
            lastSr: 0
        },
        async created() {
            fetch('/Game/MapList').then((response) => {
                response.json().then((data) => {
                    Vue.set(AddGame, "maps", data);
                    this.$nextTick(() => {
                        let select: HTMLSelectElement = <HTMLSelectElement>document.getElementById("Game_Map");
                        M.FormSelect.getInstance(select)?.destroy();
                        M.FormSelect.init(select, SELECT_FORMS_OPTIONS);
                    });
                })
            })
            
            fetch('/Game/RoleList').then((response) => {
                response.json().then((data) => {
                    Vue.set(AddGame, "roles", data);
                    this.$nextTick(() => {
                        let select: HTMLSelectElement = <HTMLSelectElement>document.getElementById("Game_Type");
                        M.FormSelect.getInstance(select)?.destroy();
                        M.FormSelect.init(select, SELECT_FORMS_OPTIONS);
                    });
                })
            })
        },
        methods: {
            async updateHeroList() {
                fetch(`/Game/HeroList?roleId=${AddGame.$data.selectedRole}`).then((response) => {
                    response.json().then((data) => {
                        Vue.set(AddGame, "heroes", data);
                        this.$nextTick(() => {
                            let select:HTMLSelectElement = <HTMLSelectElement>document.getElementById("Game_Heroes");
                            M.FormSelect.getInstance(select)?.destroy();
                            M.FormSelect.init(select, SELECT_FORMS_OPTIONS);
                        })
                    })
                })
            }
        }
    });

    const MODAL_OPTIONS: Partial<M.ModalOptions> = {
        onOpenEnd: function (el) {
            $(el).find('select2').trigger('select2:resize')
        }
    }
    let modals: NodeListOf<Element> = document.querySelectorAll('.modal')
    M.Modal.init(modals, MODAL_OPTIONS);

    const COLLAPSIBLE_OPTIONS: Partial<M.CollapsibleOptions> = {}
    let collapsibleElems: NodeListOf<Element> = document.querySelectorAll('.collapsible');
    M.Collapsible.init(collapsibleElems, COLLAPSIBLE_OPTIONS);
    
    $('.select2').select2();

    const TOOLTIP_OPTIONS: Partial<M.TooltipOptions> = {}
    let tooltipElems: NodeListOf<Element> = document.querySelectorAll('.tooltipped');
    M.Tooltip.init(tooltipElems, TOOLTIP_OPTIONS);
    
    let canvas = <NodeListOf<HTMLCanvasElement>>document.querySelectorAll(".auto-chart-js");
    
    canvas.forEach((canva) => {
        const context = canva.getContext("2d");
        if (!context)
            return;
        
        fetch(`/ChartJs/Get/${canva.id}`, {
            headers: {
                "Accept": "application/json"
            }
        })
            .then(response => response.json())
            .then((data) => {
                new Chart(context, data);
            })
            .catch(() =>  {
                if (!context.canvas.parentElement)
                    return;
                context.canvas.parentElement.innerText = "Not enough Data"
            })
    })
    
})