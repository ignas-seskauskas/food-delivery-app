import { useEffect } from "react";
import { Col, Row } from "react-bootstrap";
import { LoginForm } from "../components/users/LoginForm";
import { LoginRequestDto } from "../interfaces/login";
import { generateToken, reset } from "../features/auth/authSlice";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { useNavigate } from "react-router-dom";
import { GrantType, StateStatus, UserType } from "../interfaces/enums";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import { CreateTokenRequestDto } from "../interfaces/token";
import { Spinner } from "../components/ui/Spinner";

export function Login() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { user, status, message } = useAppSelector((state) => state.auth);

  const onSubmit = (data: LoginRequestDto) => {
    const requestDto: CreateTokenRequestDto = {
      grantType: GrantType.UsernamePassword,
      username: data.username,
      password: data.password,
      userType: data.userType,
    };
    dispatch(generateToken(requestDto));
  };

  useEffect(() => {
    if (status == StateStatus.Error && message) {
      toast.error(message);
    }

    if (user) {
      if (user.userType == UserType.Customer) {
        navigate("/stores");
      } else {
        navigate("/dashboard");
      }
    }
  }, [user, status, message]);

  useEffect(() => {
    return () => {
      dispatch(reset());
    };
  }, []);

  if (status === StateStatus.Loading) {
    return <Spinner />;
  }

  return (
    <Row className="d-flex justify-content-center">
      <Col xs={10} sm={12} md={10} lg={7} xl={6}>
        <h1 className="text-center mt-3 mb-4">Login</h1>
        <p className="text-center mt-4">
          Sign in with data you entered during registration
        </p>
        <LoginForm onSubmit={onSubmit} />
        <p className="text-center mt-4">
          Don't have an account?{" "}
          <Link to="/register" className="text-decoration-none">
            Sign up
          </Link>
        </p>
      </Col>
    </Row>
  );
}
