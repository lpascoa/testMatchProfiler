import "vuetify/styles";
import { createVuetify } from "vuetify";
import { aliases, mdi } from "vuetify/iconsets/mdi-svg";

export const asisaTheme = {
  dark: false,
  colors: {
    primary: "#004A97",
    secondary: "#24B9CB",
    complementary: "#FF5400",
    "complementary-2": "#FFAE46",
    "text-1": "#727272",
    "text-2": "#2E2E2E",
    "primary-light": "#0059B8",
    "primary-lighter": "#0063CC",
    "soft-blue": "#D6E9FF",
    "soft-blue-2": "#DEEDFF",
    "soft-blue-3": "#EBF4FF",
    "secondary-dark": "#1D9FAA",
    success: "#89CA65",
    error: "#B3001B",
    background: "#E5E5E5",
    "background-light": "#F2F2F2",
    white: "#FFFFFF",
  },
};

export default createVuetify({
  theme: {
    themes: {
      light: asisaTheme,
      dark: asisaTheme,
    },
  },
  icons: {
    defaultSet: "mdi",
    aliases,
    sets: {
      mdi,
    },
  },
});
