export const canvas = {
    namespaced: true,
    state: () => ({
        context: null,
        canvas: null,
        isDrawing: false,
        isClearing: false,
    }),

    mutations: {
        setContext(state, context) {
            state.context = context;
        },

        setCanvas(state, canvas) {
            state.canvas = canvas;
        },

        setIsDrawing(state, isDrawing) {
            state.isDrawing = isDrawing;
        },

        setIsClearing(state, isClearing) {
            state.isClearing = isClearing;
        }
    }
}