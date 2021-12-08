export const drawOptions = {
    state: () => ({
        lineColor: '',
        lineWidth: 5
    }),

    mutations: {
        setLineColor(state, lineColor) {
            state.lineColor = lineColor;
        },

        setLineWidth(state, lineWidth) {
            console.log(lineWidth);
            state.lineWidth = lineWidth;
        }
    },

    namespaced: true
}