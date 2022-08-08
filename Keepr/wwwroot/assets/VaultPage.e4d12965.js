import{I as v,u as V,G as y,c as d,a as f,o as c,b as n,d as a,t as p,p as h,F as g,n as K,q as b,s as I,e as k}from"./vendor.67942716.js";import{_ as w,v as u,l as _,P as l,A as i}from"./index.0a1ccef8.js";const x={setup(){const e=v(),s=V();return y(async()=>{try{await u.getVaultById(e.params.id),await u.getVaultKeeps(e.params.id)}catch(o){s.push({name:"Home"}),_.error(o),l.toast("Private Vault","error")}}),{account:d(()=>i.account),myKeeps:d(()=>i.myVaultKeeps),activeVault:d(()=>i.activeVault),async deleteVault(){try{await l.confirm()&&(await u.deleteVault(e.params.id),s.push({name:"Account"}),l.toast("Vault Deleted","error"))}catch(o){_.error(o),l.toast(o.message,"error")}}}}},S=e=>(b("data-v-10f33cb5"),e=e(),I(),e),P={class:"container"},A={class:"col-md-12 d-flex justify-content-between"},B={class:"mt-4"},E={class:"ms-1"},j={key:0},C=S(()=>a("div",null,null,-1)),D={class:"masonry-frame mt-3 ms-2"};function L(e,s,o,t,N,F){const m=f("VaultKeep");return c(),n("div",P,[a("div",A,[a("div",null,[a("h1",B,p(t.activeVault.name),1),a("h5",E,"Keeps: "+p(t.myKeeps.length),1)]),t.activeVault.creatorId==t.account.id?(c(),n("div",j,[a("button",{class:"btn btn-outline-danger m-4",onClick:s[0]||(s[0]=(...r)=>t.deleteVault&&t.deleteVault(...r))}," DELETE VAULT ")])):h("",!0)]),C,a("div",D,[(c(!0),n(g,null,K(t.myKeeps,r=>(c(),n("div",{key:r.id},[k(m,{keep:r},null,8,["keep"])]))),128))])])}var q=w(x,[["render",L],["__scopeId","data-v-10f33cb5"]]);export{q as default};
