import React from 'react';
import { useNavigate } from 'react-router-dom';

interface NavButtonProps {
  to: string; 
  label: string; 
  className?: string; 
}

export const NavButton: React.FC<NavButtonProps> = ({ to, label, className }) => {
  const navigate = useNavigate();


  const handleNavigation = () => {
    navigate(to);
  };

  return (
    <button 
      onClick={handleNavigation} 
      className={`nav-button ${className}`}
      style={{
        padding: '10px 20px',
        backgroundColor: '#007bff',
        color: 'white',
        border: 'none',
        borderRadius: '5px',
        cursor: 'pointer',
        fontSize: '16px',
        margin: '5px',
      }}
    >
      {label}
    </button>
  );
};