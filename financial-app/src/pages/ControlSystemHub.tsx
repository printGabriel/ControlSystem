import React from 'react';
import { useEffect, useState } from "react";
import { api } from "../services/api";
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { NavButton } from '../components/NavButton';

interface CreateUser {
    userName: string;
    userEmail: string;
    BirthDate: Date;
    balance: number;
}

export function ControlSystemHub() {
  return (
    <div className="container">
        <div className='title'>
      <h1>Sistema de controle financeiro</h1>
      <h3>Escolha o que deseja fazer:</h3>
</div>
      <nav className='center-form' style={{ padding: "20px" }}>
        <NavButton className='navButton' to="/usersform" label="Adicionar pessoas" />
        <NavButton className='navButton' to="/transactionsform" label="Adicionar transações" />
        <NavButton className='navButton' to="" label="Adicionar categorias" />
        <NavButton className='navButton' to="/financialsummary" label="Sumário de transações" />
      </nav>
    </div>
  );
}