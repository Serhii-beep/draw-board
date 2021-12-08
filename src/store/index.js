import { createStore } from "vuex";
import { drawOptions } from "@/store/drawOptions";
import { canvas } from "@/store/canvas";

export default createStore({
    state: () => ({
        connection: null,
        isConnected: false
    }),

    modules: {
        drawOptions: drawOptions,
        canvas: canvas
    }
});