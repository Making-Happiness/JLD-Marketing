import { useState, type FormEvent } from 'react';
import { Building2, Mail, Lock, Eye, EyeOff, ArrowRight, AlertCircle, ShieldCheck } from 'lucide-react';
import './LoginPage.css';

interface LoginPageProps {
  onLoginSuccess?: (email: string) => void;
}

export function LoginPage({ onLoginSuccess }: LoginPageProps) {
  const [email, setEmail] = useState<string>(() => {
    return localStorage.getItem('jld_remember_email') || 'kayeencampana@gmail.com';
  });
  const [password, setPassword] = useState<string>('admin123');
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const [rememberMe, setRememberMe] = useState<boolean>(() => {
    return !!localStorage.getItem('jld_remember_email') || true;
  });
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setErrorMessage(null);

    const trimmedEmail = email.trim().toLowerCase();
    if (!trimmedEmail) {
      setErrorMessage('Please enter your email address.');
      return;
    }

    if (!trimmedEmail.includes('@') || !trimmedEmail.includes('.')) {
      setErrorMessage('Please enter a valid email address.');
      return;
    }

    if (!password) {
      setErrorMessage('Please enter your password.');
      return;
    }

    // Gated credential verification
    if (trimmedEmail !== 'kayeencampana@gmail.com' || password !== 'admin123') {
      setErrorMessage('Invalid credentials. Authorized email: kayeencampana@gmail.com');
      return;
    }

    setIsSubmitting(true);

    if (rememberMe) {
      localStorage.setItem('jld_remember_email', trimmedEmail);
    } else {
      localStorage.removeItem('jld_remember_email');
    }

    sessionStorage.setItem('jld_auth_user', trimmedEmail);

    if (onLoginSuccess) {
      onLoginSuccess(trimmedEmail);
    } else {
      window.location.assign('/?view=workspace');
    }
  };

  return (
    <div className="login-viewport">
      <div className="login-container">
        {/* Main Floating Executive Card */}
        <div className="login-card">
          {/* Centered Brand Mark & System Crest */}
          <div className="login-brand-header">
            <div className="login-brand-badge" aria-hidden="true">
              <Building2 size={26} strokeWidth={1.75} />
            </div>
            <h1 className="login-brand-title">JLD Subdivision</h1>
            <div className="login-system-pill">
              <span className="login-status-dot" aria-hidden="true" />
              <span>Real Property Accounting ERP</span>
            </div>
          </div>

          {/* Form Context */}
          <div className="login-heading-group">
            <h2 className="login-card-title">Sign In</h2>
            <p className="login-card-subtitle">
              Enter your credentials to access the financial ledger
            </p>
          </div>

          {/* Validation Feedback */}
          {errorMessage && (
            <div className="login-alert-banner" role="alert">
              <AlertCircle size={16} />
              <span>{errorMessage}</span>
            </div>
          )}

          {/* Authentication Form */}
          <form className="login-form" onSubmit={handleSubmit} noValidate>
            <div className="login-field-item">
              <label htmlFor="login-email" className="login-label">
                Email address
              </label>
              <div className="login-input-container">
                <Mail className="login-field-icon" size={18} aria-hidden="true" />
                <input
                  id="login-email"
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  placeholder="kayeencampana@gmail.com"
                  autoComplete="email"
                  required
                  className="login-text-input"
                />
              </div>
            </div>

            <div className="login-field-item">
              <label htmlFor="login-password" className="login-label">
                Password
              </label>
              <div className="login-input-container">
                <Lock className="login-field-icon" size={18} aria-hidden="true" />
                <input
                  id="login-password"
                  type={showPassword ? 'text' : 'password'}
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="admin123"
                  autoComplete="current-password"
                  required
                  className="login-text-input login-password-input"
                />
                <button
                  type="button"
                  className="login-visibility-toggle"
                  onClick={() => setShowPassword(!showPassword)}
                  aria-label={showPassword ? 'Hide password' : 'Show password'}
                  title={showPassword ? 'Hide password' : 'Show password'}
                >
                  {showPassword ? <EyeOff size={17} /> : <Eye size={17} />}
                </button>
              </div>
            </div>

            <div className="login-meta-row">
              <label className="login-checkbox-label">
                <input
                  type="checkbox"
                  checked={rememberMe}
                  onChange={(e) => setRememberMe(e.target.checked)}
                  className="login-checkbox"
                />
                <span>Remember this device</span>
              </label>
            </div>

            <button
              type="submit"
              className="login-cta-button"
              disabled={isSubmitting}
            >
              <span>{isSubmitting ? 'Opening Workspace…' : 'Sign In to Workspace'}</span>
              <ArrowRight size={18} strokeWidth={2} />
            </button>
          </form>

          {/* Security Guarantee */}
          <div className="login-security-indicator">
            <ShieldCheck size={14} strokeWidth={2} />
            <span>Authorized Personnel Only · 256-Bit Encrypted Session</span>
          </div>
        </div>

        {/* Dignified Footer */}
        <footer className="login-viewport-footer">
          <span>© 2026 JLD Subdivision Management</span>
          <span className="login-footer-separator" aria-hidden="true">·</span>
          <span>Enterprise Real Estate Financial System</span>
        </footer>
      </div>
    </div>
  );
}



