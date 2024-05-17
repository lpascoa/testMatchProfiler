import { createRouter, createWebHistory } from "vue-router";
import ListView from "../views/ListView.vue";
import EditView from "../views/EditView.vue";

const routes = [
  {
    path: "/",
    name: "List",
    component: ListView,
  },
  {
    path: "/edit/:id",
    name: "Edit",
    component: EditView,
    props: true,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

export default router;
