import { createStore } from "vuex";
import { drawOptions } from "@/store/drawOptions";
import { HubConnectionBuilder } from '@aspnet/signalr'

export default createStore({
    state: () => ({
        connection: null,
        isConnected: false
    }),

    mutations: {
        setConnection(state) {
            if(!state.isConnected) {
                state.connection = new HubConnectionBuilder().withUrl("https://localhost:44348/draw").build();
                state.connection.start();
                state.isConnected = true;
            }
        }
    },

    modules: {
        drawOptions: drawOptions
    }
});