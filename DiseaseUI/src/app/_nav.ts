export const navItems: Array<any> = [
  {
    name: 'Dashboard',
    url: '/mis/dashboard',
    icon: 'fas fa-tv',
    roles: ["Head Office", "Admin"]
  },
  {
    name: 'Manage User',
    url: '/mis/user-management',
    icon: 'fa-solid fa-users',
    roles: ["Head Office", "Admin"]
  },
  {
    name: 'Master Configuration',
    icon: 'fa-solid fa-cog',
    url: '/mis/districts',
    roles: ["Head Office", "Admin"],
    children: [
      {
        name: 'Districts',
        url: '/mis/districts',
        icon: 'fa-solid fa-map',
        roles: ["Head Office", "Admin"]
      },
      {
        name: 'Institutions',
        url: '/mis/institutes',
        icon: 'fa-solid fa-building-columns',
        roles: ["Head Office", "Admin"]
      },
      {
        name: 'In-Charge',
        url: '/mis/in-charge',
        icon: 'fa-solid fa-address-card',
        roles: ["Head Office", "Admin"]
      },
      {
        name: 'Diseases',
        url: '/mis/diseases',
        icon: 'fa-solid fa-viruses',
        roles: ["Head Office", "Admin"]
      },
      {
        name: 'Animals',
        url: '/mis/animals',
        icon: 'fa-solid fa-paw',
        roles: ["Head Office", "Admin"]
      },
      {
        name: 'Laboratories',
        url: '/mis/laboratories',
        icon: 'fa-solid fa-flask',
        roles: ["Head Office", "Admin"]
      },
      {
        name: 'Samples',
        url: '/mis/samples',
        icon: 'fa-solid fa-vial',
        roles: ["Head Office", "Admin"]
      },
    ]
  },
  {
    name: 'Forms',
    url: 'javascript:void(0)',
    icon: 'fa-solid fa-file',
    roles: ["Head Office", "Admin", "District-Level", "Laboratory-Level"],
    children: [
      {
        name: 'Non-Infectious',
        url: '/mis/forms/non-infectious',
        icon: 'fa-solid fa-virus-covid-slash',
        roles: ["Head Office", "Admin", "District-Level"],
      },
      {
        name: 'Infectious',
        url: '/mis/forms/infectious',
        icon: 'fa-solid fa-virus-covid',
        roles: ["Head Office", "Admin", "District-Level"]
      },
      {
        name: 'Laboratory',
        url: '/mis/laboratory',
        icon: 'fa-solid fa-flask',
        roles: ["Head Office", "Admin", "Laboratory-Level"]
      }
    ]
  },
  // {
  //   name: 'Reports',
  //   icon: 'fa fa-dashboard',
  //   roles: ["Head Office", "Admin", "District-Level", "Laboratory-Level"],
  //   children: [
  //     {
  //       name: 'Non-Infectious',
  //       url: '/mis/reports',
  //       icon: 'fa-solid fa-virus-covid-slash',
  //       roles: ["Head Office", "Admin", "District-Level"],
  //     },
  //     {
  //       name: 'Infectious',
  //       url: '/mis/reports',
  //       icon: 'fa-solid fa-virus-covid',
  //       roles: ["Head Office", "Admin", "District-Level"]
  //     },
  //     {
  //       name: 'Laboratory',
  //       url: '/mis/reports',
  //       icon: 'fa-solid fa-flask',
  //       roles: ["Head Office", "Admin", "Laboratory-Level"]
  //     }
  //   ]
  // }
];
