__declspec(dllexport)
int add(int a, int b)
{
	return a + b;
}
__declspec(dllexport)
int sub(int a, int b)
{
	return a - b;
}
__declspec(dllexport)
int mult(int a, int b)
{
	return a * b;
}
__declspec(dllexport)
int div(int a, int b)
{
	if (b == 0)
	{
		throw;
	}
	return a / b;
}