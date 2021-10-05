

class Helloworld extends React.Component {
    render() {
        return <h1>Hello, {this.props.name}</h1>;
    }
}

React.render(<Helloworld name="Lekhok Bhattacharje" />, document.getElementById("Comp"));